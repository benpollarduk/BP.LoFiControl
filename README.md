# BP.LoFiControl
A WPF UserControl that displays content contained within itself in low fidelity.

## Introduction
There arent many options available for rendering WPF UIElements in artificially low resolution.
LoFiControl provides a simple control that can display content at a reduced resolution and frame rate.

## Use
TODO.

## Issues
The are currently multiple issues with this implementation:
* Rendering on LoFiMask is memory intensive and highly inefficient.
* Content with transparent backgrounds can bee seen through the mask.
