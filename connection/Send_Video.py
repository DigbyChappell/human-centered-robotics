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
        capture = cv2.VideoCapture(0)
        if capture.isOpened():
            ret, frame = capture.read()
        else:
            ret = False
        while ret:
            ret, frame = capture.read()
            frame = cv2.resize(frame, (int(frame.shape[1] / 2),
                                       int(frame.shape[0]/2)))
            encoded = cv2.imencode('.jpg', frame)[1]
            data = np.array(encoded, np.uint8).tobytes()
            print(len(data))
            self.client.send(len(data).to_bytes(8, byteorder='little'))
            self.client.send(data)
            time.sleep(1/24)
        cv2.destroyAllWindows()
        capture.release()


if __name__ == "__main__":
    s = Server()
    s.wait_for_connection()
