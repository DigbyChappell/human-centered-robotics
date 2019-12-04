import socket
import numpy as np
import time


class Client:
    def __init__(self, address=("10.0.0.70", 8053)):
        self.s = socket.socket()
        self.s.connect(address)

    def receive_data(self):
        data = self.s.recv(64)
        try:
            coordinates = np.frombuffer(data)
            print(coordinates)
        except Exception as e:
            action = data.decode("utf-8")
            print(action)
            

if __name__ == "__main__":
    c = Client()

    while True:
        c.receive_data()
