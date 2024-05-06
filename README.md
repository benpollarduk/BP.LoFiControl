# BP.LoFiControl
A WPF UserControl that displays content contained within itself in low fidelity.

## Introduction
There arent many options available for rendering WPF UIElements in artificially low resolution.
LoFiControl provides a simple control that can display content at a reduced resolution and frame rate.

## Use
```xaml
<LoFiControl Strength="3" FramesPerSecond="20">
    <Label Content="This is an example label" FontSize="20"/>
</LoFiControl>
```
![image](https://github.com/benpollarduk/BP.LoFiControl/assets/129943363/2ed37738-01c3-4a9b-b560-a2016ea162de)

## Issues
The are currently multiple issues with this implementation:
* Rendering on LoFiMask is memory intensive and highly inefficient.
* Content with transparent backgrounds can be seen through the mask.
