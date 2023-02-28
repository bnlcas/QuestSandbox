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
A sample build can be found ([here](https://drive.google.com/file/d/1hy1PVuEShyKGmQ6gyo-EnIFOR1gFE7Lt/view?usp=sharing)).

### Fingerpainting
This is mostly a sandbox to play with shaders, hand interactions and the sense of touch in AR/VR. The demo scene can be found in `/PaintBrush/Scenes/PaintbrushDemo.unity`. This consists of a set of control slides to adjust the size and color of a brush attached on the right index finger tip (no left hand support), and the ability of use that brush to paint on a surface. As a default, the image is segmented into regions of a tiger but you could change the image to have no regions or different regions. More experiments with surfaces and movement are pending. The latest build can be found ([here](https://drive.google.com/file/d/1dGjM6xFaUApzZK2kc1rmE5xTRCrs6CHH/view?usp=sharing)). A demo video can be found ([here](https://drive.google.com/file/d/1o6cW9Jp_w9CVV0BWO3DjIff-dB8I3ZhZ/view?usp=sharing))