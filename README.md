<div align="center">

# BP.LoFiControl

A WPF control that displays content in reduced fidelity. Supports .Net 8.0.

[![GitHub release](https://img.shields.io/github/release/benpollarduk/BP.LoFiControl.svg)](https://github.com/benpollarduk/BP.LoFiControl/releases)
[![License](https://img.shields.io/github/license/benpollarduk/BP.LoFiControl.svg)](https://opensource.org/licenses/MIT)

</div>

## Introduction
There aren't many options available for rendering WPF UIElements in artificially low resolution.
LoFiPresenter provides a simple control that can display content at a reduced resolution and frame rate.

## Use
```xml
<LoFiPresenter Reduction="3" FramesPerSecond="20">
    <Label Content="This is an example label" FontSize="20"/>
</LoFiPresenter>
```

## Example
Reduction 1:

![image](https://github.com/benpollarduk/BP.LoFiControl/assets/129943363/cfb4cdf6-2657-4e38-aeff-04612c1cf7a8)

Reduction 2:

![image](https://github.com/benpollarduk/BP.LoFiControl/assets/129943363/f4208e65-53af-49c2-8f59-7fd60d6dc024)

Reduction 3:

![image](https://github.com/benpollarduk/BP.LoFiControl/assets/129943363/a63ba834-fa3f-459f-877f-7fd89363e139)

Reduction 4:

![image](https://github.com/benpollarduk/BP.LoFiControl/assets/129943363/264664e1-e06b-4359-bd25-5504bd0bdcaf)

Reduction 5:

![image](https://github.com/benpollarduk/BP.LoFiControl/assets/129943363/0468753b-727b-4ea0-ab15-c044d6110ea2)

## How it works
LoFiControl is a simple codebase with 2 main classes, LoFiPresenter and LoFiMask.
* LoFiPresenter hosts WPF content and a LoFiMask.
* LoFiMask creates a bitmap at a lower resolution from the hosted content.
* LoFiMask then renders this bitmap as its background at the same size as the hostesd content using BitmapScalingMode.NearestNeighbor to get the pixelated effect.

## Issues
The are currently two main issues with this implementation:
* Rendering on LoFiMask is memory intensive and inefficient.
* Content with transparent backgrounds can be seen through the mask.
