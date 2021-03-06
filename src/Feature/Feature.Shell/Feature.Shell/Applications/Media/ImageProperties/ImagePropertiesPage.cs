using Sitecore.Configuration;
using Sitecore.Controls;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Xaml;
using System;
using System.Web.UI.WebControls;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace Feature.Shell.Applications.Media.ImageProperties
{
    /// <summary>Represents a GridDesignerPage.</summary>
    public class ImagePropertiesPage : DialogPage
    {
        /// <summary>The alt.</summary>
        protected TextBox Alt;
        /// <summary>The aspect.</summary>
        protected Sitecore.Web.UI.HtmlControls.Checkbox Aspect;
        /// <summary>The h space.</summary>
        protected TextBox HSpace;
        /// <summary>The height edit.</summary>
        protected TextBox HeightEdit;
        /// <summary>The original size.</summary>
        protected Sitecore.Web.UI.HtmlControls.Literal OriginalSize;
        /// <summary>The original text.</summary>
        protected TextBox OriginalText;
        /// <summary>The size warning.</summary>
        protected Border SizeWarning;
        /// <summary>The v space.</summary>
        protected TextBox VSpace;
        /// <summary>The width edit.</summary>
        protected TextBox WidthEdit;
        /// <summary>The crowdsource alt text</summary>
        protected DropDownList CrowdsourcedDropDownList;

        /// <summary>Gets or sets the height of the image.</summary>
        /// <value>The height of the image.</value>
        public int ImageHeight
        {
            get
            {
                return (int)this.ViewState[nameof(ImageHeight)];
            }
            set
            {
                this.ViewState[nameof(ImageHeight)] = (object)value;
            }
        }

        /// <summary>Gets or sets the width of the image.</summary>
        /// <value>The width of the image.</value>
        public int ImageWidth
        {
            get
            {
                return (int)this.ViewState[nameof(ImageWidth)];
            }
            set
            {
                this.ViewState[nameof(ImageWidth)] = (object)value;
            }
        }

        /// <summary>Gets or sets the XML value.</summary>
        /// <value>The XML value.</value>
        /// <contract>
        ///   <requires name="value" condition="not null" />
        ///   <ensures condition="nullable" />
        /// </contract>
        private XmlValue XmlValue
        {
            get
            {
                return new XmlValue(Sitecore.StringUtil.GetString(this.ViewState[nameof(XmlValue)]), "image");
            }
            set
            {
                Assert.ArgumentNotNull((object)value, nameof(value));
                this.ViewState[nameof(XmlValue)] = (object)value.ToString();
            }
        }

        /// <summary>Changes the height.</summary>
        protected void ChangeHeight()
        {
            if (this.ImageHeight == 0)
                return;
            int num = Sitecore.MainUtil.GetInt(this.HeightEdit.Text, 0);
            if (num > 0)
            {
                if (num > 8192)
                {
                    num = 8192;
                    this.HeightEdit.Text = "8192";
                    SheerResponse.SetAttribute(this.HeightEdit.ClientID, "value", this.HeightEdit.Text);
                }
                if (this.Aspect.Checked)
                {
                    this.WidthEdit.Text = ((int)((double)num / (double)this.ImageHeight * (double)this.ImageWidth)).ToString();
                    SheerResponse.SetAttribute(this.WidthEdit.ClientID, "value", this.WidthEdit.Text);
                }
            }
            SheerResponse.SetReturnValue(true);
        }

        
        /// <summary>Changes the width.</summary>
        protected void ChangeWidth()
        {
            if (this.ImageWidth == 0)
                return;
            int num = Sitecore.MainUtil.GetInt(this.WidthEdit.Text, 0);
            if (num > 0)
            {
                if (num > 8192)
                {
                    num = 8192;
                    this.WidthEdit.Text = "8192";
                    SheerResponse.SetAttribute(this.WidthEdit.ClientID, "value", this.WidthEdit.Text);
                }
                if (this.Aspect.Checked)
                {
                    this.HeightEdit.Text = ((int)((double)num / (double)this.ImageWidth * (double)this.ImageHeight)).ToString();
                    SheerResponse.SetAttribute(this.HeightEdit.ClientID, "value", this.HeightEdit.Text);
                }
            }
            SheerResponse.SetReturnValue(true);
        }

        protected void ChangeCrowdsourcedAlt()
        {
            if (this.CrowdsourcedDropDownList.SelectedIndex > 0)
            {
                this.Alt.Text = this.CrowdsourcedDropDownList.SelectedValue;
                SheerResponse.SetAttribute(this.Alt.ClientID, "value", this.CrowdsourcedDropDownList.SelectedValue);
            }
            SheerResponse.SetReturnValue(true);
        }

        /// <summary>Handles a click on the OK button.</summary>
        /// <remarks>When the user clicks OK, the dialog is closed by calling
        /// the <see cref="M:Sitecore.Web.UI.Sheer.ClientResponse.CloseWindow">CloseWindow</see> method.</remarks>
        protected override void OK_Click()
        {
            XmlValue xmlValue = this.XmlValue;
            Assert.IsNotNull((object)xmlValue, "XmlValue");
            xmlValue.SetAttribute("alt", this.Alt.Text);
            xmlValue.SetAttribute("height", this.HeightEdit.Text);
            xmlValue.SetAttribute("width", this.WidthEdit.Text);
            xmlValue.SetAttribute("hspace", this.HSpace.Text);
            xmlValue.SetAttribute("vspace", this.VSpace.Text);
            SheerResponse.SetDialogValue(xmlValue.ToString());
            base.OK_Click();
        }

        /// <summary>Raises the <see cref="E:System.Web.UI.Control.Load"></see> event.</summary>
        /// <param name="e">The <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull((object)e, nameof(e));
            base.OnLoad(e);
            if (XamlControl.AjaxScriptManager.IsEvent)
                return;
            this.ImageWidth = 0;
            this.ImageHeight = 0;
            ItemUri queryString = ItemUri.ParseQueryString();
            if (queryString == (ItemUri)null)
                return;
            Item obj = Database.GetItem(queryString);
            if (obj == null)
                return;
            string text = obj["Dimensions"];
            if (!string.IsNullOrEmpty(text))
            {
                int length = text.IndexOf('x');
                if (length >= 0)
                {
                    this.ImageWidth = Sitecore.MainUtil.GetInt(Sitecore.StringUtil.Left(text, length).Trim(), 0);
                    this.ImageHeight = Sitecore.MainUtil.GetInt(Sitecore.StringUtil.Mid(text, length + 1).Trim(), 0);
                }
            }
            if (this.ImageWidth <= 0 || this.ImageHeight <= 0)
            {
                this.Aspect.Checked = false;
                this.Aspect.Disabled = true;
            }
            else
                this.Aspect.Checked = true;
            if (this.ImageWidth > 0)
                this.OriginalSize.Text = Translate.Text("Original Dimensions: {0} x {1}", (object)this.ImageWidth, (object)this.ImageHeight);
            if (Sitecore.MainUtil.GetLong((object)obj["Size"], 0L) >= Settings.Media.MaxSizeInMemory)
            {
                this.HeightEdit.Enabled = false;
                this.WidthEdit.Enabled = false;
                this.Aspect.Disabled = true;
            }
            else
                this.SizeWarning.Visible = false;
            this.OriginalText.Text = Sitecore.StringUtil.GetString(obj["Alt"], Translate.Text("[none]"));
            UrlHandle urlHandle = UrlHandle.Get();
            XmlValue xmlValue = new XmlValue(urlHandle["xmlvalue"], "image");
            this.XmlValue = xmlValue;
            this.Alt.Text = xmlValue.GetAttribute("alt");
            this.HeightEdit.Text = xmlValue.GetAttribute("height");
            this.WidthEdit.Text = xmlValue.GetAttribute("width");
            this.HSpace.Text = xmlValue.GetAttribute("hspace");
            this.VSpace.Text = xmlValue.GetAttribute("vspace");
            if (Sitecore.MainUtil.GetBool(urlHandle["disableheight"], false))
            {
                this.HeightEdit.Enabled = false;
                this.Aspect.Checked = false;
                this.Aspect.Disabled = true;
            }

            //Azure=azurevalue&human1=human1value&huma2=human2value
            var nameValueString = obj["CrowdSourced Alt Text"];

            var nameValueList = WebUtil.ParseUrlParameters(nameValueString);

            this.CrowdsourcedDropDownList.Items.Insert(0, new ListItem("--- SELECT ---"));

            //Apply logic
            int index = 1;

            var items = nameValueList.AllKeys;

            foreach (string key in items)
            {
                var value = nameValueList[key];

                var listItem = $"{key}: {value}";

                this.CrowdsourcedDropDownList.Items.Insert(index++, new ListItem(listItem));
            }

            if (!Sitecore.MainUtil.GetBool(urlHandle["disablewidth"], false))
                return;
            this.WidthEdit.Enabled = false;
            this.Aspect.Checked = false;
            this.Aspect.Disabled = true;

           
        }
    }
}
