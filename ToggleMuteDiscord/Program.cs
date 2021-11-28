using NAudio.CoreAudioApi;

bool singleDev = args.Length == 0;
bool muted;

if (singleDev)
{
    var volumes = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)
        .Select(p => p.AudioEndpointVolume).ToList();
    muted = volumes.Any(d => d.Mute);
    volumes.ForEach(d => d.Mute = !muted);
    volumes.ForEach(d => d.Dispose());
}
else
{
    var volume = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications).AudioEndpointVolume;
    muted = volume.Mute;
    volume.Mute = !muted;
    volume.Dispose();
}

new System.Media.SoundPlayer(!muted ? ToggleMuteDiscord.Properties.Resources.Mutesound : ToggleMuteDiscord.Properties.Resources.Unmutesound).PlaySync();