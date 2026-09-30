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

    private WaveStream? waveStream;

    public SoundPlayer(string filePath, bool looping = true)
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
            waveStream = new AudioFileReader(filePath);
            
        }   
        else if (filePath.EndsWith(".mp3"))
        {
            Mp3FileReaderBase.FrameDecompressorBuilder builder = new Mp3FileReaderBase.FrameDecompressorBuilder(waveFormat => new Mp3FrameDecompressor(waveFormat));
            waveStream = new Mp3FileReaderBase(filePath, builder);
        }

        if (waveStream != null)
        {
            if (looping)
            {
                waveStream = new LoopStream(waveStream);
            }
            outputDevice.Init(waveStream);
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
        waveStream?.Dispose();
    }
}
