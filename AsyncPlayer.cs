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
        IWavePlayer waveOutDevice = new WaveOut(); 
        public int playDuration=0;// TO DO: сохранение приостановленной позиции
        static object locker = new object();

        public AsyncPlayer(string filePath)
        {
            FilePath = filePath;
            waveOutDevice.PlaybackStopped += Play;
        }

        public void Dispose()
        {
        }
        public void EnqueueTask(VkNet.Model.Attachments.Audio audio)
        {
            queue.Enqueue(audio);
            DownloadFile(audio.Url);
        }
        public void Play(object sender, EventArgs e)
        {
            if ((queue.Count != 0) && (waveOutDevice.PlaybackState != PlaybackState.Playing))
            {
                AudioFileReader audioFileReader = new AudioFileReader(FilePath + "\\" + Path.GetFileName(queue.Dequeue().Url.AbsolutePath));
                waveOutDevice.Init(audioFileReader);
            }
            waveOutDevice.Play();
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
            if (queue.Count < 2)
                my_WebClient.DownloadFile(fileUri, File_Path);
            else
                my_WebClient.DownloadFileAsync(fileUri, File_Path);
            return File_Path;
        }
    }
}