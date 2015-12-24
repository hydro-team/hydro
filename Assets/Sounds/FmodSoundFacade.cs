using UnityEngine;
using FMOD.Studio;

namespace Sounds {

    /// <summary>
    /// Uses the FMOD Studio sound engine.
    /// </summary>
    public class FmodSoundFacade : MonoBehaviour, SoundFacade {

        Sound SoundFacade.this[string soundName] {
            get { return Get(soundName); }
        }

        Sound SoundFacade.Play(string soundName) {
            var sound = Get(soundName);
            sound.Play();
            return sound;
        }

        Sound Get(string soundName) {
            var sound = FMODUnity.RuntimeManager.CreateInstance("event:" + soundName);
            return new FmodSound(soundName, sound);
        }

        class FmodSound : Sound {

            readonly string name;
            readonly EventInstance sound;

            public FmodSound(string name, EventInstance sound) {
                this.name = name;
                this.sound = sound;
            }

            string Sound.Name() {
                return name;
            }

            void Sound.Play() {
                sound.start();
            }

            void Sound.Restart() {
                sound.start();
            }

            void Sound.Stop() {
                sound.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}
