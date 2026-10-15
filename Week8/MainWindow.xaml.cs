using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media;

namespace MusicReferenceHub;

// MainWindow is a class. Put fields and methods inside these outer braces.
public partial class MainWindow : Window
{
    private bool audioLoaded = false;
    public MainWindow()
    {
        InitializeComponent();
        // TODO: connect your collection in the Data chapter.
    }

    private void Load_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Audio Files|*.mp3;*.wav";

    if (dialog.ShowDialog() == true)
    {
    mediaPlayer.Source = new Uri(dialog.FileName);
    audioLoaded = true;
    txtStatus.Text = "Selected: " + dialog.SafeFileName;
    }
    else
    {
    txtStatus.Text = "No file selected.";
    }
    }

    private void Play_Click(object sender, RoutedEventArgs e)
    {
        if (audioLoaded)
        {
            mediaPlayer.Play();
            txtStatus.Text = "Playing music.";
        }
        else
        {
            txtStatus.Text = "No audio loaded. Please load an audio file first.";
        }
    }

    private void Pause_Click(object sender, RoutedEventArgs e)
    {
        if (audioLoaded)
        {
            mediaPlayer.Pause();
            txtStatus.Text = "Music paused.";
        }
        else
        {
            txtStatus.Text = "No audio loaded.";
        }
    }  
    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        if (audioLoaded)
        {
            mediaPlayer.Stop();
            txtStatus.Text = "Music stopped.";
        }
        else
        {
            txtStatus.Text = "No audio loaded.";
        }
    }

    private void AddLink_Click(object sender, RoutedEventArgs e)
    {
        string link = txtLink.Text.Trim();
        if (Uri.TryCreate(link, UriKind.Absolute, out Uri? url)
            && (url.Scheme == Uri.UriSchemeHttp || 
            url.Scheme == Uri.UriSchemeHttps))
        {
            lstLinks.Items.Add(link);
            txtLink.Clear();
            txtStatus.Text = "Link added successfully.";
        }
        else
        {
            txtStatus.Text = "Please enter a valid URL.";
        }
    }

    private void OpenLink_Click(object sender, RoutedEventArgs e)
    {
        if (lstLinks.SelectedItem is string link)
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo
                {
                    FileName = link,
                    UseShellExecute = true
                });
            txtStatus.Text = "Opening link.";
        }
        else
        {
            txtStatus.Text = "Please select a link first.";
        }
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        string notes = txtNotes.Text.Trim();

        if (string.IsNullOrWhiteSpace(notes))
        {
            txtStatus.Text = "Please enter notes before saving.";
            return;
        }

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "Text Files (*.txt)|*.txt";
        dialog.FileName = "MusicNotes.txt";

        if (dialog.ShowDialog() == true)
        {
            try
            {
                System.IO.File.WriteAllText(dialog.FileName, notes);
                txtStatus.Text = "Notes saved successfully.";
            }
            catch (Exception)
            {
                txtStatus.Text = "Unable to save notes.";
            }
        }
    }

    private void Open_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Text Files (*.txt)|*.txt";

        if (dialog.ShowDialog() == true)
        {
            try
            {
                txtNotes.Text = System.IO.File.ReadAllText(dialog.FileName);
                txtStatus.Text = "Notes reopened successfully.";
            }
            catch (Exception)
            {
                 txtStatus.Text = "Unable to open notes.";
            }
        }
    }

    private void Image_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Image Files |*.jpg;*.jpeg;*.png;";

        if (dialog.ShowDialog() == true)
        {
            imgArtwork.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(dialog.FileName));
            txtStatus.Text = "Artwork loaded successfully.";
        }
        else
        {
            txtStatus.Text = "No artwork selected.";
        }
    }

    private void Media_Opened(object sender, RoutedEventArgs e)
    {
        txtStatus.Text = "Audio opened successfully.";
    }

    private void Media_Ended(object sender, RoutedEventArgs e)
    {
        txtStatus.Text = "Music finished playing.";
    }

    private void Media_Failed(object sender, ExceptionRoutedEventArgs e)
    {
        txtStatus.Text = "Unable to play audio. Please check the file format.";
    }
}
