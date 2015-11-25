namespace Sound {

    /// <summary>
    /// Abstracts the sound subsystem with a simple interface.
    /// To create a sound instance, use the array operator [].
    /// To create a sound already started, use the Play method.
    /// </summary>
    public interface SoundFacade {

        /// <summary>Creates a new sound instance</summary>
        Sound this[string soundName] { get; }

        /// <summary>Creates a new sound instance already started</summary>
        Sound Play(string soundName);
    }
}
