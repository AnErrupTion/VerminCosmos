# Vermin
My old Cosmos OS, codenamed Vermin.

By the way, I'm now using MOSA (https://github.com/mosa/MOSA-Project), and the VBEWorld demo has got a window manager and cool stuff like that! And much less buggy than the one in this project!

This project is cut into 3 parts:<br/><br/>
**CosmosVGA**: The VGA driver used by Vermin, based off https://github.com/napalmtorch/CosmosVGA. It incorporated a few patches and initial work for a higher resolution, but never really got finished.<br/><br/>
**VBETest**: A prototype which was going to use VBE instead of VGA to draw, but never got finished either.<br/><br/>
**Vermin**: The actual OS. The redraw method used here is re-using parts of the screen, instead of making full repaints of it. This can improve performance if the drawing methods are well optimized (however, I don't know if that will have any positive performance impact in this case). Besides that, it is filled with bugs. (Again, fixed in the VBEWorld demo in MOSA!)
