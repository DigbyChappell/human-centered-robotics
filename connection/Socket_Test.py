import socket, time, os, random


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
        x = open("test.txt", "rb")
        while True:
            data = x.read(1024)
            if data == b'':
                print("finished")
                x.close()
                self.client.close()
                break
            self.client.send(data)


if __name__ == "__main__":
    s = Server()
    s.wait_for_connection()
