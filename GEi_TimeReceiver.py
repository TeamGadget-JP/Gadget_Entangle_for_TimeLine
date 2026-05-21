import RLPy
import socket
import ctypes
import sys
from PySide2.QtWidgets import *
from PySide2.QtCore import *

class GETL_iClone_Receiver(QWidget):
    def __init__(self):
        super(GETL_iClone_Receiver, self).__init__()
        self.setWindowTitle("Team Gadget : GEi Sync")
        self.setGeometry(100, 100, 320, 150)
        self.setWindowFlags(Qt.WindowStaysOnTopHint) # Always at the forefront
        
        layout = QVBoxLayout()
        self.title_label = QLabel("<b>GETL Receiver</b>")
        self.title_label.setAlignment(Qt.AlignCenter)
        layout.addWidget(self.title_label)
        
        self.status_label = QLabel("Status: <font color='red'>STOPPED</font>")
        self.status_label.setAlignment(Qt.AlignCenter)
        layout.addWidget(self.status_label)
        
        self.start_btn = QPushButton("▶ START SYNC (Port: 8992)")
        self.start_btn.setStyleSheet("background-color: #2E7D32; color: white; font-weight: bold; padding: 10px;")
        self.start_btn.clicked.connect(self.start_sync)
        layout.addWidget(self.start_btn)
        
        self.stop_btn = QPushButton("■ STOP SYNC")
        self.stop_btn.setStyleSheet("background-color: #C62828; color: white; font-weight: bold; padding: 10px;")
        self.stop_btn.clicked.connect(self.stop_sync)
        layout.addWidget(self.stop_btn)
        self.setLayout(layout)
        
        self.udp_port = 8992
        
        # Windows API Timer
        HWND = ctypes.c_void_p
        UINT = ctypes.c_uint
        UINT_PTR = ctypes.c_uint64 if sys.maxsize > 2**32 else ctypes.c_uint
        DWORD = ctypes.c_ulong
        self.TIMERPROC = ctypes.WINFUNCTYPE(None, HWND, UINT, UINT_PTR, DWORD)
        
        self.user32 = ctypes.windll.user32
        self.user32.SetTimer.argtypes = [HWND, UINT_PTR, UINT, self.TIMERPROC]
        self.user32.SetTimer.restype = UINT_PTR
        self.user32.KillTimer.argtypes = [HWND, UINT_PTR]
        self.user32.KillTimer.restype = ctypes.c_bool

    def start_sync(self):
        if not hasattr(sys, 'getl_ic_sock'):
            sys.getl_ic_sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
            sys.getl_ic_sock.bind(("127.0.0.1", self.udp_port))
            sys.getl_ic_sock.setblocking(False)
            
        sys.getl_last_frame = -1
        sys.getl_ic_timer_func = self.TIMERPROC(self.tick)
        
        if getattr(sys, 'getl_ic_timer_id', None) is None:
            # Tuning: Ultra-high-speed monitoring at 5ms (approximately 200fps)
            timer_id = self.user32.SetTimer(None, 0, 5, sys.getl_ic_timer_func)
            if timer_id:
                sys.getl_ic_timer_id = timer_id
                
        self.status_label.setText("<b>Status: <font color='green'>SYNCING</font></b>")
        print("▶️ [GETL] Started Sync on port 8992.")
        
    def stop_sync(self):
        if getattr(sys, 'getl_ic_timer_id', None) is not None:
            self.user32.KillTimer(None, sys.getl_ic_timer_id)
            sys.getl_ic_timer_id = None
            
        if hasattr(sys, 'getl_ic_sock'):
            sys.getl_ic_sock.close()
            del sys.getl_ic_sock
            
        self.status_label.setText("<b>Status: <font color='red'>STOPPED</font></b>")
        print("⏹️ [GETL] Sync stopped.")
        
    def tick(self, hwnd, msg, timer_id, current_time):
        if not hasattr(sys, 'getl_ic_sock'): return
        latest_frame = None
        
        while True:
            try:
                data, addr = sys.getl_ic_sock.recvfrom(1024)
                msg_str = data.decode('utf-8')
                if msg_str.startswith("GETL"):
                    parts = msg_str.split(",")
                    if len(parts) >= 3:
                        latest_frame = int(parts[1])
            except BlockingIOError:
                break
            except Exception:
                break
                
        if latest_frame is not None:
            # Never touch it when the frame hasn't changed (maintain zero load).
            if latest_frame == getattr(sys, 'getl_last_frame', -1):
                return
            
            try:
                if hasattr(RLPy.RTime, "FromValue"):
                    target_ticks = int((latest_frame / 60.0) * 6000.0)
                    time_obj = RLPy.RTime.FromValue(target_ticks)
                    
                    # 1. Rewrite time
                    RLPy.RGlobal.SetTime(time_obj)
                    sys.getl_last_frame = latest_frame
                    
                    # 2. The screen is forced to redraw "only" at the moment the time changes.
                    QApplication.processEvents()
            except Exception as e:
                pass

    def closeEvent(self, event):
        self.stop_sync()
        super(GETL_iClone_Receiver, self).closeEvent(event)

if 'getl_iclone_window' not in globals():
    getl_iclone_window = None
if getl_iclone_window is not None:
    getl_iclone_window.close()

getl_iclone_window = GETL_iClone_Receiver()
getl_iclone_window.show()
