//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VkNet;
//using NAudio;
//using NAudio.Wave;
//using System.IO;
//using System.Threading;
//using System.Net;

//namespace Vk_Music_Player
//{
//    // TO DO: по возможности получше подумать и переписать класс
//    class AsyncPlayer : IDisposable
//    {
//        static EventWaitHandle wh = new AutoResetEvent(false);

//        public static Queue<VkNet.Model.Attachments.Audio> queue = new Queue<VkNet.Model.Attachments.Audio>();
//        public VkNet.Model.Attachments.Audio currAudio;
//        public static string FilePath = "C:\\Users\\Andrey\\Music\\tmpItems";// TO DO: добавить публичное свойство с проверкой корректности, свободного места и т.д.
//        public static IWavePlayer waveOutDevice = new WaveOut(); 
//        public int playDuration=0;// TO DO: сохранение приостановленной позиции
//        static object locker = new object();
//        Thread worker;
//        static WebClient my_WebClient;

//        public AsyncPlayer()
//        {
//            worker = new Thread(this.Play);
//            worker.Start();
//        }

//        public void Dispose()
//        {
//            lock (locker)
//                EnqueueTask(null);
//        }
//        public static void EnqueueTask(object audio1)
//        {
//            VkNet.Model.Attachments.Audio audio = (VkNet.Model.Attachments.Audio)audio1;
//            lock (locker)
//            {
//                DownloadFile((Uri)audio.Url);// TO DO: загрузка файлов
//                queue.Enqueue(audio);
//            }
//            wh.Set();
//        }

//        void readAudio()
//        {
//            currAudio = queue.Dequeue();
//            string filePath = FilePath + "\\" + Path.GetFileName(((Uri)currAudio.Url).AbsolutePath);
//            if (!File.Exists(filePath)||my_WebClient.IsBusy)
//            {
//                EnqueueTask(currAudio);
//                while (!File.Exists(filePath)||my_WebClient.IsBusy)
//                    Thread.Sleep(1000);
//            }
//            else
//            {
//                queue.Enqueue(currAudio);
//            }
//            AudioFileReader audioFileReader = new AudioFileReader(filePath);
//            waveOutDevice.Init(audioFileReader);
//        }

//        public void Play()
//        {
//            lock (locker)
//            {
//                if (queue.Count > 0)
//                {
//                    if (waveOutDevice.PlaybackState != PlaybackState.Playing)
//                    {
//                        readAudio();
//                    }
//                    waveOutDevice.Play();
//                    //Thread.Sleep(currAudio.Duration * 1000);
//                }
//                else
//                    wh.WaitOne();
//            }
//        }
//        public void Stop()
//        {
//            if (waveOutDevice.PlaybackState != PlaybackState.Stopped)
//            {
//                waveOutDevice.Stop();
//                //worker.Abort();
//            }
//        }

//        public void Pause()
//        {
//            if (waveOutDevice.PlaybackState != PlaybackState.Stopped)
//            {
//                if (waveOutDevice.PlaybackState == PlaybackState.Playing)
//                {
//                    waveOutDevice.Pause();
//                }
//                else if (waveOutDevice.PlaybackState == PlaybackState.Paused)
//                {
//                    waveOutDevice.Play();
//                }
//            }
//        }

//        // TO DO: вынести загрузку файлов в отдельный класс
//        static void DownloadFile(Uri fileUri)
//        {
//            my_WebClient = new WebClient();
//            string File_Path = FilePath + "\\" + Path.GetFileName(fileUri.AbsolutePath);
//            my_WebClient.DownloadFile(fileUri, File_Path);
//        }
//        public void getVolume(int v)
//        {
//            waveOutDevice.Volume = ((float)v)/100;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using NAudio;
using NAudio.Wave;
using System.IO;
using System.Threading;
using System.Net;

namespace Vk_Music_Player
{
    // TO DO: по возможности получше подумать и переписать класс
    class AsyncPlayer
    {
        static EventWaitHandle wh = new AutoResetEvent(false);

        public static Queue<VkNet.Model.Attachments.Audio> queue = new Queue<VkNet.Model.Attachments.Audio>();
        public VkNet.Model.Attachments.Audio currAudio;
        public string FilePath;// TO DO: добавить публичное свойство с проверкой корректности, свободного места и т.д.
        public IWavePlayer waveOutDevice = new WaveOut();
        public int playDuration = 0;// TO DO: сохранение приостановленной позиции
        static object locker = new object();

        public AsyncPlayer(string filePath)
        {
            FilePath = filePath;
            waveOutDevice.PlaybackStopped += Play;
        }

        public void Dispose()
        {
        }
        public void EnqueueTask(object audio1)
        {
            VkNet.Model.Attachments.Audio audio = (VkNet.Model.Attachments.Audio)audio1;
            queue.Enqueue(audio);
            DownloadFile(audio.Url);
        }
        public void Play(object sender, EventArgs e)
        {
            if ((queue.Count != 0) && (waveOutDevice.PlaybackState != PlaybackState.Playing))
            {
                AudioFileReader audioFileReader = new AudioFileReader(FilePath + "\\" + Path.GetFileName(queue.Dequeue().Url.AbsolutePath));
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
            }
        }

        public void Pause()
        {
            if (waveOutDevice.PlaybackState == PlaybackState.Playing)
                waveOutDevice.Pause();
        }

        public void Stop()
        {
            if (waveOutDevice.PlaybackState != PlaybackState.Stopped)
            {
                waveOutDevice.Stop();
            }
        }

        // TO DO: вынести загрузку файлов в отдельный класс
        string DownloadFile(Uri fileUri)
        {
            WebClient my_WebClient = new WebClient();
            string File_Path = FilePath + "\\" + Path.GetFileName(fileUri.AbsolutePath);
            if (!File.Exists(File_Path))
            {
                if (queue.Count < 2)
                    my_WebClient.DownloadFile(fileUri, File_Path);
                else
                    my_WebClient.DownloadFileAsync(fileUri, File_Path);
            }
            return File_Path;
        }

        public void getVolume(int v)
        {
            waveOutDevice.Volume = ((float)v) / 100;
        }
    }
}