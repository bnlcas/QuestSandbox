# Quest Sandbox

Repository for experiments with Oculus Quest.

## Setup
Download the Quest SDK and Configure according to their latest docs.

## Experiments:

### Video -> 3D ML
A demo can be found in the `/DepthVideoPlayer/Scenes/DepthPlayerSample.unity`
In order to produce this effect with your own videos, use ([this 
colab](https://colab.research.google.com/drive/1YZ4kcOWXuCbwcRGL58hrjiYUBaa4uiTA#scrollTo=ALVgKMNfZ0UW))
The Clip and the resulting Depth Map will need to be placed in the `Resources` 
Folder. and linked in the scene. You can add multiple clips and cycle through them with the Oculus Right Controller Buttons (also basic grab manipulations).
