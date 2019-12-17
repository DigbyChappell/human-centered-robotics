import socket
import numpy as np
import time
import speech_recognition as sr
import threading
import pyaudio


def thread_function():
    r = sr.Recognizer()
    with sr.Microphone() as source:
        r.energy_threshold = 20000
        while True:
            try:
                print("Say something!")
                audio = r.listen(source, timeout=1, phrase_time_limit=2)
                print("Listened")
                spoken = r.recognize_google(audio)
                print("Recognised")
                print(spoken)
            except Exception as e:
                print(e)
                pass


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
        except Exception as e:
            pass

class Server:
    def __init__(self, address=("10.0.0.70", 8053), max_client=1):
        self.s = socket.socket()
        self.s.bind(address)
        self.s.listen(max_client)
        self.client, self.adr = self.s.accept()

    def send_coordinates(self, coordinates):
        data = coordinates.tobytes()
        self.client.send(data)

    def send_action(self, action):
        data = bytes(action, 'utf-8')
        self.client.send(data)
        time.sleep(0.1)
        self.client.send(bytes('aa', 'utf-8'))

            

if __name__ == "__main__":
##    c = Client()
##    print('Connected to Unity')
##    s = Server()
    print('Connected to Robot laptop')
    x = threading.Thread(target=thread_function)
    x.start()

    while True:
        time.sleep(0.01)
