# Music Reference Hub

## Main Features

- Open a local audio file.
- Save and open reference links.
- Save, export, and reopen session notes.
- Reject blank or invalid input and show a status message.

## Build and Run

This is a WPF app, so it must be run on Windows. On Windows, open
`Week8/MusicReferenceHub.csproj` in Visual Studio and click **Start**.

## AI check

One precise unknown: How can I stop the Play button from trying to play when no audio file has been loaded?

Smallest useful change: In Week8/MainWindow.xaml.cs, add this check at the start of your Play_Click method, before its existing play command. Your screenshot shows you already have an audioLoaded variable.

One test: Click Play before loading a file. The message should appear, and the app should stay open. Then load an audio file and click Play again.

if (!audioLoaded)
{
    MessageBox.Show("Load an audio file first.");
    return;
}

rejected the code I didn't want to mess with my code.

## Week 8 Evidence 
My Week 8 and supporting evidence can be found in the Evidence folder.

[View Week 8A Evidence](Evidence/Week8A/)

[View Week 8B Evidence](Evidence/Week8B/)

## Receipt
Receipt can be found in my Evidence folder

[View my Week 8 Receipt](Evidence/Receipt/)
