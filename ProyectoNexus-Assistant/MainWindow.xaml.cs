using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Recognition; //Reconocedor
using System.Speech.Synthesis; // NeXus



namespace ProyectosNexusAlpha
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
        SpeechSynthesizer NeXus = new SpeechSynthesizer();
        Random r = new Random();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void loaded_Loaded(object sender, RoutedEventArgs e)
        {
            rec.SetInputToDefaultAudioDevice();

            Choices comandos = new Choices(new string[] {"hola NeXus", "como estas", "gracias", "abre Chrome", "abre paint" });

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(comandos);

            Grammar gramatica = new Grammar(gb);

            rec.LoadGrammar(gramatica);
            rec.RecognizeAsync(RecognizeMode.Multiple);

            rec.SpeechRecognized += Rec_SpeechRecognized;
            rec.AudioLevelUpdated += Rec_AudioLevelUpdated;

        }

        private void Rec_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
           pbAudio.Value = e.AudioLevel;
        }

        private void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //NeXus.Speak(e.Result.Text);
            
            lblTextoReconocido.Content = e.Result.Text;
            
            switch (e.Result.Text)
            {
                case "hola NeXus":
                    NeXus.Speak("Hola jefe, como estas?");
                    break;

                case "abre Chrome":
                    NeXus.SpeakAsync("Abriendo Chrome");
                    System.Diagnostics.Process.Start("chrome.exe");
                    NeXus.Speak("Chrome Abierto");
                    break;

                case ("Gracias"):
                    int valor = r.Next(1, 4);
                    if (valor == 1)
                    {
                        NeXus.Speak("para eso estamos");
                    }
                    if (valor == 2)
                    {
                        NeXus.Speak("De nada, jefecito");
                    } 
                    if (valor == 3)
                    {
                        NeXus.Speak("ahora me das las gracias, que cara dura");
                    }
                    break;
                case ("abre paint"):
                    NeXus.SpeakAsync("abriendo paint");
                    System.Diagnostics.Process.Start("mspaint.exe");
                    NeXus.Speak("paint abierto");
                    break;
                
                default:
                    break;
            }
        }
    }
}
