# WinUI3Preview3_Problems_SetTitleBar

This repository contains two solutions with sample code demonstrating the missing Title Bar capability that is available in UWP but not available in WinUI3 Preview 3 Desktop.

The UWP sample code works as expected with code that updates the Background and Foreground colors of the Title Bar. However, the WinUI3 Desktop sample code does not allow the Title Bar to be customized by using SetTitleBar method in the MainPage due the compile error "Window does not contain definition for 'SetTitleBar'."

----

**Describe the bug**

In WinUI3 Preview3, the Background and Foreground of the window Title Bar cannot be customized by using SetTitleBar method in the MainPage due the compile error "Window does not contain definition for 'SetTitleBar'."

**Steps to reproduce the bug**
1. Clone the [WinUI3 Preview 3 Problems Set Title Bar repository](https://github.com/eleanorleffler/WinUI3Preview3_Problems_SetTitleBar).
2. Go to the SetTitleBarWinUIPreview3 folder.
3. Open the SetTitleBarWinUIPreview3 solution in Visual Studio 2019 Preview.
4. Build and run with Debug x64.
5. Click through the different buttons to update the Title Bar and view the different variations and scenarios of Title Bar customization.

	A. The "Home Page" and "Second Page" buttons demonstrate the ability to update the text and color of the Title Bar - However in WinUI3 Preview 3, the method to update the Title Bar does not compile and thus is commented out, and we do not see the Title Bar text color change.
	
	B. The "File Picker" button demonstrates that the Title Bar will be inactive including the main window buttons. Again, the customization code for color is commented out so we do not see the lighter shade of blue on the Title Bar like we would in UWP.
	
	C. The "Content Dialog" button tests that the Title Bar will be inactive except for main window buttons.
	
	D. The "Content Dialog w/File Picker" button tests that Title Bar will be inactive except for main window buttons.

**Expected behavior**

We expect there to be a programmatic way to customize the Title Bar and for the customization to stay persistent in all of these scenarios.

**Screenshots**

Side by Side Comparisons between UWP and WinUI3 Preview 3

**Version Info**

NuGet package version: 

[Microsoft.VCRTForwarders.140 1.0.6]
[Microsoft.WinUI 3.0.0-preview3.201113.0]

Targeting:
Target: Universal Windows
Target version: Windows 10, version 1809 (10.0; Build 17763)
Min version: Windows 10, version 1803 (10.0; Build 17134)

Windows app type:
| UWP              | Win32            |
| :--------------- | :--------------- |
| 		Yes 	   |  				  |

| Windows 10 version                  | Saw the problem? |
| :--------------------------------- | :-------------------- |
| Insider Build (xxxxx)              | 						 |
| May 2020 Update (19041)            | 						 |
| November 2019 Update (18363)       | 						 |
| May 2019 Update (18362)            | 						 |
| October 2018 Update (17763)        | 			Yes			 |
| April 2018 Update (17134)          | 						 |
| Fall Creators Update (16299)       | 						 |
| Creators Update (15063)            | 						 |

| Device form factor | Saw the problem? |
| :----------------- | :--------------- |
| Desktop            | 		Yes			|
| Xbox               | 					|
| Surface Hub        | 					|
| IoT                | 					|

**Additional context**
