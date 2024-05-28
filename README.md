# Master Audio System
Script designed to organize the audio clips that will be played in a Unity game.

### **How it works:**
It should be attached to a Manager object (for example). When the game is running, an empty object with the AudioSource component (without an audio clip) will be created. Whenever an audio clip is played, this clip will search for one of the created empty objects with AudioSource and play the clip on it.
