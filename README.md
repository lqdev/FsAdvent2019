# FSharp Advent 2019

This application is part of [F# Advent 2019](https://sergeytihon.com/2019/11/05/f-advent-calendar-in-english-2019/).

The purpose of it is to train a machine learning multiclass classification model that categorizes bookmarked links using ML.NET.

## Prerequisites

This application was built on a Windows 10 PC, but should work cross-platform.

- .NET Core SDK
- Visual Studio Code
- Ionide
- Microsoft.ML NuGet Package

## Get the data

[Click on this link to download and unzip the data anywhere on your PC](https://archive.ics.uci.edu/ml/machine-learning-databases/00359/NewsAggregatorDataset.zip).

The data contains information about several articles that are separated into four categories: business (b), science and technology (t), entertainment (e) and health (h). Visit the [UCI Machine Learning repository website](https://archive.ics.uci.edu/ml/datasets/News+Aggregator) to learn more about the dataset.

Below is a sample of the data.

```text
ID    Title    Url    Publisher    Category    Story    Hostname    Timestamp
2	Fed's Charles Plosser sees high bar for change in pace of tapering	http://www.livemint.com/Politics/H2EvwJSK2VE6OF7iK1g3PP/Feds-Charles-Plosser-sees-high-bar-for-change-in-pace-of-ta.html	Livemint	b	ddUyU0VZz0BRneMioxUPQVP6sIxvM	www.livemint.com	1394470371207
3	US open: Stocks fall after Fed official hints at accelerated tapering	http://www.ifamagazine.com/news/us-open-stocks-fall-after-fed-official-hints-at-accelerated-tapering-294436	IFA Magazine	b	ddUyU0VZz0BRneMioxUPQVP6sIxvM	www.ifamagazine.com	1394470371550
4	Fed risks falling 'behind the curve', Charles Plosser says	http://www.ifamagazine.com/news/fed-risks-falling-behind-the-curve-charles-plosser-says-294430	IFA Magazine	b	ddUyU0VZz0BRneMioxUPQVP6sIxvM	www.ifamagazine.com	1394470371793
```

Inside the console application directory, create a new directory called *data* and copy the *newsCorpora.csv* file to it.

```powershell
mkdir data
```

## Run the application

Navigate to the console application directory and enter the following command into the terminal:

```bash
dotnet run
```

