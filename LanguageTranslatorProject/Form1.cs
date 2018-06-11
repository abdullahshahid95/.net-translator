using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Media;

using NAudio;
using NAudio.Wave;

using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Apis.Translate.v2.Data;
using TranslationsResource = Google.Apis.Translate.v2.Data.TranslationsResource;

using Google.Apis.Speech.v1beta1;
using Google.Apis.Speech.v1beta1.Data;




namespace LanguageTranslatorProject
{
    public partial class Form1 : Form
    {
        string detect=null;
        String srcText;

        Uri urltts;
        string mp3path;
        string wavpath;
        string text;
        WebClient tts;
        Mp3FileReader reader;

        
        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        void willuse()
        {
            var service = new TranslateService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAsjrVKsPasvqfcZnvrIayprnql5zEIt0M", // your API key, that you get from Google Developer Console
                ApplicationName = "API key 1" // your application name, that you get form Google Developer Console
            });

            
            srcText = textBox1.Text;
       
             String[] target_language_shortname = new String[] {"af", "sq", "am", "ar", "hy", "az", "eu", "be", "bn", "bs", 
                                                                "bg", "ca", "ceb", "ny", "zh-CN", "zh-TW", "co", "hr", "cs", "da", 
                                                                "nl", "en", "eo", "et", "tl", "fi", "fr", "fy", "gl", "ka", 
                                                                "de", "el", "gu", "ht", "ha", "haw", "iw", "hi", "hmn", "hu", 
                                                                "is", "ig", "id", "ga", "it", "ja", "jw", "kn", "kk", "km", 
                                                                "ko", "ku", "ky", "lo", "la", "lv", "lt", "lb", "mk", "mg", 
                                                                "ms", "ml", "mt", "mi", "mr", "mn", "my", "ne", "no", "ps", 
                                                                "fa", "pl", "pt", "ma", "ro", "ru", "sm", "gd", "sr", "st", 
                                                                "sn", "sd", "si", "sk", "sl", "so", "es", "su", "sw", "sv", 
                                                                "tg", "ta", "te", "th", "tr", "uk", "ur", "uz", "vi", "cy", 
                                                                "xh", "yi", "yo", "zu"};



             try
             {
                 TranslationsListResponse response = service.Translations.List(srcText, target_language_shortname[comboBox2.SelectedIndex]).Execute();



                 foreach (TranslationsResource t in response.Translations)
                 {
                     textBox2.Text = t.TranslatedText;
                     
                 }


                 
             }

             catch (IndexOutOfRangeException)
             {
                 MessageBox.Show("Please select a language");
             }

             catch (Google.GoogleApiException)
             {
                 MessageBox.Show("Please enter the source text");

             }


             
        }

        void listen1()
        {
            srcText = textBox1.Text;
            String[] target_language_shortname = new String[] { "af", "sq", "am", "ar", "hy", "az", "eu", "be", "bn", "bs", 
                                                                "bg", "ca", "ceb", "ny", "zh-CN", "zh-TW", "co", "hr", "cs", "da", 
                                                                "nl", "en", "eo", "et", "tl", "fi", "fr", "fy", "gl", "ka", 
                                                                "de", "el", "gu", "ht", "ha", "haw", "iw", "hi", "hmn", "hu", 
                                                                "is", "ig", "id", "ga", "it", "ja", "jw", "kn", "kk", "km", 
                                                                "ko", "ku", "ky", "lo", "la", "lv", "lt", "lb", "mk", "mg", 
                                                                "ms", "ml", "mt", "mi", "mr", "mn", "my", "ne", "no", "ps", 
                                                                "fa", "pl", "pt", "ma", "ro", "ru", "sm", "gd", "sr", "st", 
                                                                "sn", "sd", "si", "sk", "sl", "so", "es", "su", "sw", "sv", 
                                                                "tg", "ta", "te", "th", "tr", "uk", "ur", "uz", "vi", "cy", 
                                                               "xh", "yi", "yo", "zu"};

            var service = new TranslateService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAsjrVKsPasvqfcZnvrIayprnql5zEIt0M", // your API key, that you get from Google Developer Console
                ApplicationName = "API key 1" // your application name, that you get form Google Developer Console
            });

            TranslationsListResponse response = service.Translations.List(srcText, "en").Execute();

            foreach (TranslationsResource t in response.Translations)
            {

                detect = t.DetectedSourceLanguage;
            }
            
            
            try
            {
                text = textBox1.Text;
                mp3path = Environment.CurrentDirectory + @"tmp.mp3";
                wavpath = Environment.CurrentDirectory + @"\me\me.wav";

                urltts = new Uri("http://translate.google.com/translate_tts?client=tw-ob&tl="+detect+"-gb&q=" + text);

                using (tts = new WebClient())
                {
                    tts.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 9.0; Windows;)");
                    tts.DownloadFile(urltts, mp3path);
                }

                using (reader = new Mp3FileReader(new FileStream(mp3path, FileMode.OpenOrCreate)))
                {
                    WaveFileWriter.CreateWaveFile(wavpath, reader);
                }

                SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\Abdullah\Documents\Visual Studio 2013\Projects\LanguageTranslatorProject\LanguageTranslatorProject\bin\Debug\me\me.wav");
                simpleSound.Play();

                //MessageBox.Show("hogya");
            }

            catch
            {
                MessageBox.Show("Error. Clear the screen first.\nNote that some languages are not supported.");
            }
        
            
        }

        void listen2()
        {
            String[] target_language_shortnamee = new String[] { "af", "sq", "am", "ar", "hy", "az", "eu", "be", "bn", "bs", 
                                                                "bg", "ca", "ceb", "ny", "zh-CN", "zh-TW", "co", "hr", "cs", "da", 
                                                                "nl", "en", "eo", "et", "tl", "fi", "fr", "fy", "gl", "ka", 
                                                                "de", "el", "gu", "ht", "ha", "haw", "iw", "hi", "hmn", "hu", 
                                                                "is", "ig", "id", "ga", "it", "ja", "jw", "kn", "kk", "km", 
                                                                "ko", "ku", "ky", "lo", "la", "lv", "lt", "lb", "mk", "mg", 
                                                                "ms", "ml", "mt", "mi", "mr", "mn", "my", "ne", "no", "ps", 
                                                                "fa", "pl", "pt", "ma", "ro", "ru", "sm", "gd", "sr", "st", 
                                                                "sn", "sd", "si", "sk", "sl", "so", "es", "su", "sw", "sv", 
                                                                "tg", "ta", "te", "th", "tr", "uk", "ur", "uz", "vi", "cy", 
                                                               "xh", "yi", "yo", "zu"};
           try
            {
                text = textBox2.Text;
                mp3path = Environment.CurrentDirectory + @"tmp.mp3";
                wavpath = Environment.CurrentDirectory + @"\me\me.wav";

                urltts = new Uri("http://translate.google.com/translate_tts?client=tw-ob&tl="+target_language_shortnamee[comboBox2.SelectedIndex]+"-gb&q=" + text);

                using (tts = new WebClient())
                {
                    tts.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 9.0; Windows;)");
                    tts.DownloadFile(urltts, mp3path);
                }

                using (reader = new Mp3FileReader(new FileStream(mp3path, FileMode.OpenOrCreate)))
                {
                    WaveFileWriter.CreateWaveFile(wavpath, reader);
                }

                SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\Abdullah\Documents\Visual Studio 2013\Projects\LanguageTranslatorProject\LanguageTranslatorProject\bin\Debug\me\me.wav");
                simpleSound.Play();  

                //MessageBox.Show("hogya");
            }

            catch
            {
                MessageBox.Show("Error. Clear the screen first.\nNote that Some languages are not supported.");
            }


        }
        void Gspeech()
        {
            

    }



        //}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listen1();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Gspeech();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            willuse();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listen2();
        }
    }
}
