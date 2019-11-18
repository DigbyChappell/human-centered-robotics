import socket


class Client:
    def __init__(self, address=("146.169.193.161", 5000)):
        self.s = socket.socket()
        self.s.connect(address)
        self.receive_data()

    def receive_data(self):
        x = open('out.txt', 'wb')
        while True:
            data = ''
            data = self.s.recv(1024)
            if data == b'':
                break
            x.write(data)


if __name__ == "__main__":
    c = Client()
