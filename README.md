# BP.LoFiControl
A WPF control that displays content in reduced fidelity. Supports .Net 8.0.

## Introduction
There aren't many options available for rendering WPF UIElements in artificially low resolution.
LoFiPresenter provides a simple control that can display content at a reduced resolution and frame rate.

## Use
```xaml
<LoFiPresenter Reduction="3" FramesPerSecond="20">
    <Label Content="This is an example label" FontSize="20"/>
</LoFiPresenter>
```
![image](https://github.com/benpollarduk/BP.LoFiControl/assets/129943363/2ed37738-01c3-4a9b-b560-a2016ea162de)

## How it works

## Issues
The are currently two main issues with this implementation:
* Rendering on LoFiMask is memory intensive and inefficient.
* Content with transparent backgrounds can be seen through the mask.
