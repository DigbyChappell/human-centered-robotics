#
#   Hello World server in Python
#   Binds REP socket to tcp://*:5555
#   Expects b"Hello" from client, replies with b"World"
#

import time
import zmq

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")

while True:
    socket.send(b"O")   # Send origin request
    message = socket.recv()
    print("Received msg: %s" % message)

    socket.send(b"D")  # Send gaze request
    message = socket.recv()
    print("Received msg: %s" % message)
