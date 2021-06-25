[![Contributors][contributors-shield]][contributors-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
    <li><a href="#build-status">Build Status</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

[![Homuai Screen Shot][product-screenshot]](https://example.com)

When I was living with some friends (three to be more specific) I used to have some problems with the organization and the houseworks. Unfortunately, student life is so busy and it’s normal to forget things like: paying rent; and letting products expire in the refrigerator.

I didn’t find one good App that really suited my needs, so I thought: why can’t I make a project to solve this myself? Homuai is an app free of charge whose main goal is to help people who live with friends. It was a way that I found as a developer to create an opportunity to practice and improve my skills and learn new ones.

Of course, your needs may be different and I can’t promise you that this project is the best in the entire world. So I'll be adding more features in the near future. You may also suggest changes by forking this repo and creating a pull request or opening an issue.

### Built With

![xamarin-shield]
![netcore-shield]
![mysql-shield]
![aws-shield]
![android-shield]
![ios-shield]
![windows-shield]
![ubuntu-shield]
![visualstudio-shield]
![figma-shield]

<!-- GETTING STARTED -->
## Getting Started

You can download the App for free on:

* [GooglePlay](https://example.com)
* [App Store](https://example.com)

To get a local copy up and running follow these simple example steps.

### Prerequisites

* SDK Android API 23 or higher to run the Android App version

* MAC with the iOS SDK 10 (just in case you want to run the iOS App version)

* Visual Studio 2019+

* MySQL Server

### Installation

1. Clone the repo;
2. Fill all information in appsettings.Development.json. NOTE: In the section ConnectionStrings, write your database name separately from the connection, this works to create the database in the start up;
3. Run the Web API;

To run the app using the API locally do the follow:

1. Download and configure ngrok (https://ngrok.com);
2. Run ngrok;
3. Change the api link in the file RestEndPoints.cs on the app project to the link shown in the console (the console that you are running ngrok);
4. Have a good time testing.

<!-- ROADMAP -->
## Roadmap

You can see the [open issues](https://github.com/welissonArley/Homuai/issues) (and known issues) and the [board project](https://github.com/welissonArley/Homuai/projects/1) to see the future features available on the project.

<!-- LICENSE -->
## License

The Homuai Project can not be copied and/or distributed without the express permission of Welisson Arley <welissonarleyvs@gmail.com>.

Feel free to use this project to study and help me improve them by becoming a contributor :smile:

<!-- CONTACT -->
## Contact

Support: homuai@homuai.com

Website: comming soon

<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [Othneil Drew for this incredible Read-me template](https://github.com/othneildrew/Best-README-Template)
* [Willian Rodrigues with the tests](https://www.linkedin.com/in/willian-rodrigues-b99b76b7/)
* [Henrique Couto with the tests](https://www.linkedin.com/in/henrique-couto-3287b1133/)
* [Marina Moreira with the translations](https://www.linkedin.com/in/marina-moreira-54b4b116a/)

<!-- Build Status (Badges) -->
## Build Status
![Build Status](https://img.shields.io/github/workflow/status/welissonarley/Homuai/workflows/dotnet.yml/master?label=master&style=for-the-badge)
![Build Status](https://img.shields.io/github/workflow/status/welissonarley/Homuai/workflows/dotnet.yml/develop?label=develop&style=for-the-badge)

<!-- MARKDOWN LINKS & IMAGES -->
[product-screenshot]: readme-images/screenshot.png
[contributors-shield]: https://img.shields.io/github/contributors/welissonArley/Homuai.svg?style=for-the-badge
[contributors-url]: https://github.com/welissonArley/Homuai/graphs/contributors
[stars-shield]: https://img.shields.io/github/stars/welissonArley/Homuai.svg?style=for-the-badge
[stars-url]: https://github.com/welissonArley/Homuai/stargazers
[issues-shield]: https://img.shields.io/github/issues/welissonArley/Homuai.svg?style=for-the-badge
[issues-url]: https://github.com/welissonArley/Homuai/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/welissonarley/

[xamarin-shield]: https://img.shields.io/badge/Xamarin-3498DB?style=for-the-badge&logo=xamarin&logoColor=white
[netcore-shield]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[mysql-shield]: https://img.shields.io/badge/MySQL-00000F?style=for-the-badge&logo=mysql&logoColor=white
[aws-shield]: https://img.shields.io/badge/Amazon_AWS-232F3E?style=for-the-badge&logo=amazon-aws&logoColor=white
[android-shield]: https://img.shields.io/badge/Android-3DDC84?style=for-the-badge&logo=android&logoColor=white
[ios-shield]: https://img.shields.io/badge/iOS-000000?style=for-the-badge&logo=ios&logoColor=white
[windows-shield]: https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white
[ubuntu-shield]: https://img.shields.io/badge/Ubuntu-E95420?style=for-the-badge&logo=ubuntu&logoColor=white
[visualstudio-shield]: https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white
[figma-shield]: https://img.shields.io/badge/Figma-F24E1E?style=for-the-badge&logo=figma&logoColor=white
[buymecoffe-shield]: https://img.shields.io/badge/Buy_Me_A_Coffee-FFDD00?style=for-the-badge&logo=buy-me-a-coffee&logoColor=black