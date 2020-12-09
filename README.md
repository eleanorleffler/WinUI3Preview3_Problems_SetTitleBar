# WinUI3Preview3_Problems_SetTitleBar

This repository contains two solutions with sample code demonstrating the missing Title Bar capability that is available in UWP but not available in WinUI3 Preview 3 Desktop.

The UWP sample code works as expected with code that updates the Background and Foreground colors of the Title Bar. However, the WinUI3 Desktop sample code does not allow the Title Bar to be customized by using SetTitleBar method in the MainPage due the compile error "Window does not contain definition for 'SetTitleBar'."