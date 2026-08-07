import csc
import socket
import sys
import ctypes

def name():
    return "GETL TimeLine Receiver"

def description():
    return "Receives timeline sync data from Unity GETL_Broadcaster."

def run(scene):
    HWND = ctypes.c_void_p
    UINT = ctypes.c_uint
    UINT_PTR = ctypes.c_uint64 if sys.maxsize > 2**32 else ctypes.c_uint
    DWORD = ctypes.c_ulong
    TIMERPROC = ctypes.WINFUNCTYPE(None, HWND, UINT, UINT_PTR, DWORD)

    user32 = ctypes.windll.user32
    user32.SetTimer.argtypes = [HWND, UINT_PTR, UINT, TIMERPROC]
    user32.SetTimer.restype = UINT_PTR
    user32.KillTimer.argtypes = [HWND, UINT_PTR]
    user32.KillTimer.restype = ctypes.c_bool

    if not hasattr(sys, 'getl_recv_sock'):
        sys.getl_recv_sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        sys.getl_recv_sock.bind(("127.0.0.1", 8991))
        sys.getl_recv_sock.setblocking(False) 

    def sync_callback(hwnd, msg, timer_id, current_time):
        try:
            latest_frame = None
            while True:
                try:
                    data, addr = sys.getl_recv_sock.recvfrom(1024)
                    msg_str = data.decode('utf-8')
                    if msg_str.startswith("GETL"):
                        parts = msg_str.split(",")
                        if len(parts) >= 3:
                            latest_frame = int(parts[1])
                except BlockingIOError:
                    break 
                except Exception as e:
                    break
            
            if latest_frame is not None:
                try:
                    app = csc.app.get_application()
                    curr_scene = app.current_scene()
                    if curr_scene:
                        curr_scene.domain_scene().set_current_frame(latest_frame)
                except Exception as e:
                    pass
        except Exception as e:
            pass

    sys.getl_timer_func = TIMERPROC(sync_callback)

    if getattr(sys, 'getl_timer_id', None) is not None:
        user32.KillTimer(None, sys.getl_timer_id)
        sys.getl_timer_id = None
        if hasattr(sys, 'getl_recv_sock'): 
            sys.getl_recv_sock.close()
            del sys.getl_recv_sock
        print("⏹️ [GETL TimeLine Receiver] Stopped syncing with Unity.")
    else:
        timer_id = user32.SetTimer(None, 0, 16, sys.getl_timer_func)
        if timer_id:
            sys.getl_timer_id = timer_id
            print("▶️ [GETL TimeLine Receiver] Started syncing with Unity! (Port: 8991)")
