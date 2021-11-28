// See https://aka.ms/new-console-template for more information
#define SINGLEDEV

using NAudio.CoreAudioApi;

#if !SINGLEDEV
var mmdevices = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).Select(p => p.AudioEndpointVolume).ToList();
var muted = mmdevices.Any(d => d.Mute);
mmdevices.ForEach(d => d.Mute = !muted);
mmdevices.ForEach(d => d.Dispose());
#else
var mmdevices = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications).AudioEndpointVolume;
var muted = mmdevices.Mute;
mmdevices.Mute = !muted; 
mmdevices.Dispose();
#endif

new System.Media.SoundPlayer(!muted ? ToggleMuteDiscord.Properties.Resources.Mutesound : ToggleMuteDiscord.Properties.Resources.Unmutesound).PlaySync();