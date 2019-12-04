import socket
import numpy as np
import time


class Client:
    def __init__(self, address=("10.0.0.70", 8052)):
        self.s = socket.socket()
        self.s.connect(address)
        self.coordinates = np.zeros(2)

    def receive_data(self):
        data = self.s.recv(64).decode("utf-8")
        try:
            X = float(data[data.find("[")+2:data.find("Y")])
            Y = float(data[data.find("Y")+1:data.find("]")])
            self.coordinates = np.array([X, Y])
            print(self.coordinates)
        except Exception as e:
            pass

class Server:
    def __init__(self, address=("10.0.0.70", 8053), max_client=1):
        self.s = socket.socket()
        self.s.bind(address)
        self.s.listen(max_client)
        self.client, self.adr = self.s.accept()

    def send_data(self, coordinates):
        data = coordinates.tobytes()
        self.client.send(data)

            

if __name__ == "__main__":
    c = Client()
    print('Connected to Unity')
    s = Server()
    print('Connected to Robot laptop')

    while True:
        c.receive_data()
        s.send_data(c.coordinates)
