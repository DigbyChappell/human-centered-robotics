# human-centered-robotics
Group repository for Human Centered Robotics coursework.

### How to use
#### Robot Computer
Requirements:\
Kinova Gen3 robot arm\
ROS Melodic\
Python 3.x\
Rospy\
Xeoma

Instructions:
1. Connect to the same network as the Human Computer
2. Start Xeoma
    1. Select your preferred camera
    2. Press the settings button and add a web server
    3. Write down the IP and port settings, and the URL of the imageN.jpeg
    4. Change the camera quality to low for improved streaming
3. Wait for the Human Computer to be set up and ready
4. Change the IP address of the client to match the IP address found in step 2.iii.
5. Start your roscore
6. Run client.py
7. Run controller.py

#### Human Computer
Requirements:\
HTC Vive Pro Eye\
Unity 2018\
VisualStudio 2019\
SteamVR\
SR Runtime\
Python 3.x\

Instructions:
1. Connect to the same network as the Robot Computer
2. Open command prompt and run 'ipconfig' to find your network IP address
3. Start SteamVR, SR Runtime, and connect the Vive Pro Eye
4. Open 'human_computer/unity/MyScripts/webcam.cs'
    1. Change 'url' to the correct image URL from Xeoma
5. Open 'human_computer/unity/MyScripts/showData.cs'
    1. Change the line `tcpListener = new TcpListener(IPAddress.Parse("10.0.0.70"), 8052);`
    to contain your IP address found in step 2.
6. Open the Unity Scene 'WORKSscene' and press the play button
7. Open 'human_computer/python/communication.py' in a Python IDE
    1. Change the Client IP address to match your IP address found in step 2.
    2. Change the Server IP address to match your IP address found in step 2.
    3. Run communication.py