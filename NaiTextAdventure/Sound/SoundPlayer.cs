using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using NAudio.Wave;
using NAudio.Wave.Alsa;
using NLayer.NAudioSupport;

namespace Nai.TextAdventure.Sound;
public class SoundPlayer
{
    private IWavePlayer outputDevice;

    private AudioFileReader? wavFileReader;

    private Mp3FileReaderBase? mp3FileReader;

    public SoundPlayer(string filePath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            outputDevice = new AlsaOut();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            outputDevice = new WasapiPlayerBuilder().Build();
        }
        else
        {
            throw new Exception($"No sound support for OS identifier {RuntimeInformation.RuntimeIdentifier}");
        }

        if (filePath.EndsWith(".wav"))
        {
            wavFileReader = new AudioFileReader(filePath);
            outputDevice.Init(wavFileReader);
        }   
        else if (filePath.EndsWith(".mp3"))
        {
            var builder = new Mp3FileReaderBase.FrameDecompressorBuilder(waveFormat => new Mp3FrameDecompressor(waveFormat));
            mp3FileReader = new Mp3FileReaderBase(filePath, builder);

            outputDevice.Init(mp3FileReader);
        }
        

    }
    public void Play()
    {
        outputDevice.Play();
    }

    public void Stop()
    {
        outputDevice.Stop();
        outputDevice.Dispose();
        if (mp3FileReader != null)
        {
            mp3FileReader.Dispose();
        }
        else if (wavFileReader != null)
        {
            wavFileReader.Dispose();
        }
    }
}
