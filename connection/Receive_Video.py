import socket
import cv2
import numpy as np
import time


class Client:
    def __init__(self, address=("146.169.193.161", 5000)):
        self.s = socket.socket()
        self.s.connect(address)
        self.receive_data()

    def receive_data(self):
        while True:
            data_len = self.s.recv(8)
            data_len = int.from_bytes(data_len, byteorder='little')
            print(data_len)
            d = self.s.recv(data_len)
            data = np.frombuffer(d, dtype=np.uint8)
            img = cv2.imdecode(data, cv2.IMREAD_COLOR)
            cv2.imshow('Live Video Feed', img)
            cv2.waitKey(1)


if __name__ == "__main__":
    c = Client()
