import socket, time, os, random
import cv2
import numpy as np


class Server:
    def __init__(self, address=("146.169.193.161", 5000), max_client=1):
        self.s = socket.socket()
        self.s.bind(address)
        self.s.listen(max_client)

    def wait_for_connection(self):
        self.client, self.adr = self.s.accept()
        print('Got a connection from: '+str(self.client)+'.')
        self.send_data()

    def send_data(self):
        coordinates = np.array([0.12223, 0.1, 0], dtype='float64')
        while True:
            data = coordinates.tobytes()
            self.client.send(data)
            time.sleep(2)


if __name__ == "__main__":
    s = Server()
    s.wait_for_connection()
