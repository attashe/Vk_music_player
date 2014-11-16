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
    class AsyncPlayer : IDisposable
    {
        EventWaitHandle wh = new AutoResetEvent(false);

        public static Queue<VkNet.Model.Attachments.Audio> queue = new Queue<VkNet.Model.Attachments.Audio>();
        public VkNet.Model.Attachments.Audio currAudio;
        String FilePath;// TO DO: добавить публичное свойство с проверкой корректности, свободного места и т.д.
        IWavePlayer waveOutDevice = new WaveOut(); 
        public int playDuration;// TO DO: сохранение приостановленной позиции
        static object locker = new object();
        Thread worker;

        public AsyncPlayer()
        {
            worker = new Thread(Play);
            worker.Start();
        }

        public void Dispose()
        {
            lock (locker)
                EnqueueTask(null);
        }
        public static void EnqueueTask(VkNet.Model.Attachments.Audio audio)
        {
            lock (locker)
            {
                DownloadFile((Uri)currAudio.Url);// TO DO: загрузка файлов
                queue.Enqueue(audio);
            }
            wh.Set();
        }

        void readAudio()
        {
            currAudio = queue.Dequeue();
            string filePath = FilePath + "\\" + Path.GetFileName(((Uri)currAudio.Url).AbsolutePath);
            AudioFileReader audioFileReader = new AudioFileReader(filePath);
            waveOutDevice.Init(audioFileReader);
            return;
        }
        
        public void Play()
        {
            while (true)
            {
                lock (locker)
                {
                    if (queue.Count > 0)
                    {
                        if (waveOutDevice.PlaybackState != PlaybackState.Paused)
                        {
                            readAudio();
                        }
                        waveOutDevice.Play();

                        Thread.Sleep(currAudio.Duration * 1000);
                    }
                    else
                        wh.WaitOne();
                    
                }
            }
        }

        // TO DO: вынести загрузку файлов в отдельный класс
        void DownloadFile(Uri fileUri)
        {
            WebClient my_WebClient = new WebClient();
            string File_Path = FilePath + "\\" + Path.GetFileName(fileUri.AbsolutePath);
            my_WebClient.DownloadFile(fileUri, File_Path);
        }
    }
}
