import socket
import numpy as np
import time


class Client:
    def __init__(self, address=("10.0.0.70", 8053)):
        self.s = socket.socket()
        self.s.connect(address)

    def receive_data(self):
        data = self.s.recv(64)
        data = np.frombuffer(data)
        print(coordinates)
            

if __name__ == "__main__":
    c = Client()

    while True:
        c.receive_data()
