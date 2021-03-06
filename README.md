![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2021

- MUST READ: **[Submission requirements](SUBMISSION_REQUIREMENTS.md)**
- [Entry form template](ENTRYFORM.md)
- [Starter kit instructions](STARTERKIT_INSTRUCTIONS.md)
  

# Hackathon Submission Entry form

## Team name
Lady Verndalians

## Category
The best enhancement to the Sitecore Admin (XP) for Content Editors & Marketers

## Description
Module Purpose : To provide crowd-sourced alt text for images.

What problem was solved? : It is clear that machine learning image recognition for alt text is still evolving and requires human review. However, companies may be too overwhelmed to review and correct auto alt text.
During this unpresedented pandemic, we have seen the most unemployemnt since the Great Depression.
- digital demand

How does this module solve it?
How about we get overwhelmed companies and unemployed people together with crowd-sourcing image alt text?
We provide an easy way for uneomployed people to get incetivised by companies who simultaneously are experiencing increased engagement and humanized alt text for images.

---Pic : Sandy's power paint SS.

## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_
Flow :
- Retrieve auto alt text for image via Cognitive Services. (Auto-tag upon image upload.)
- Humanized alt text for images via website. : 2 (Incorporate payment gateway - ie Venmo, incorporate tweet to encourage others, add an image gallery where people can mass update alt text for images. Allow visitors to vote for the best caption/alt text; Add in text analysis to filter out inappropriate language)
- Content editor review of alt text in the image media and article image properties. (Rank the alt text by popularity/votes - in media item as well as in the image properties on the content item; auto-populate most popular)
- Select an alt text tag on one of the images on the content items.

⟹ [Replace this Video link](#video-link)


## Pre-requisites and Dependencies

⟹ Does your module rely on other Sitecore modules or frameworks?

- Azure Cognitive Service Computer Vision API, Free Limited Subscription


## Installation instructions

- This assumes you have Sitecore XP 10.1.0 set up on your work station.
- Get solution from the repo.
- Restore nuget packages and build the solution.
- Configure publishing profile to match your setup.
- Update \src\Project\Website\App_Config\Include\Project\Project.Sitedefinition.config to match you site host. i.e. : hackathon2021sc.dev.local
- Publish solution to site.
- Login to Sitecore Admin and install Sitecore packages located at \src\Project\Website\App_Data\packages
- Smart Publish the site.
- I have left my subscription keys for Cognitive Services for limited usage and review of module.
- Alternatively follow the document below_ to set up your own subscription for Cognitive Services.
- [Cognitive Service Setup](docs/Sign up for an Azure Account And Set Up Cognitive Service.docx)


Disclaimer: 

> - Sitecore Package files
  


### Configuration
⟹ NONE


## Usage instructions
- Browse the site. Navigate to Articles page/s from homepage. i.e. https://hackathon2021sc.dev.local/Articles/Article-1
- Follow the video to experiment with humanized alt text.


Include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](docs/images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## Comments
If you'd like to make additional comments that is important for your module entry.

