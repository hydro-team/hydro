using UnityEngine;
using System.Collections;

namespace Sounds {

    /// <summary>A sound instance created through a SoundFacade</summary>
    public interface Sound {

        /// <summary>The string that identifies the sound type</summary>
        string Name();

        /// <summary>Starts the sound</summary>
        void Play();

        /// <summary>Stops and starts the sound</summary>
        void Restart();

        /// <summary>Interrupts the sound</summary>
        void Stop();
    }
}
