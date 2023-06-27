import cv2 #Opencv
import mediapipe as mp #Google
import time
import matplotlib.pyplot as plt
import numpy as np
import threading

from Captura import Captura
from MallaFacial import MallaFacial
from AnalisisFacial import AnalisisFacial

def main():
    #tipoEntrada=input("Entrada video:")
    type_video_input="webcam"
    capture_object=Captura(type_video_input)
    capture=capture_object.getCaptura()
    face_mesh_object=MallaFacial()
    mediap_face_mesh,face_mesh=face_mesh_object.getMallaFacial()
    mediap_draw_points,draw_points=face_mesh_object.getPuntosMallaFacial()   
    analize_video(capture,mediap_draw_points,draw_points,mediap_face_mesh,face_mesh) 

def detect_hand_raised(hand_landmarks):
    wrist_left = hand_landmarks.landmark[mp.solutions.hands.HandLandmark.WRIST].x
    wrist_right = hand_landmarks.landmark[mp.solutions.hands.HandLandmark.WRIST].x
    thumb_tip_left = hand_landmarks.landmark[mp.solutions.hands.HandLandmark.THUMB_TIP].y
    thumb_tip_right = hand_landmarks.landmark[mp.solutions.hands.HandLandmark.THUMB_TIP].y
    index_finger_tip_left = hand_landmarks.landmark[mp.solutions.hands.HandLandmark.INDEX_FINGER_TIP].y
    index_finger_tip_right = hand_landmarks.landmark[mp.solutions.hands.HandLandmark.INDEX_FINGER_TIP].y

    if wrist_left < index_finger_tip_left and wrist_left < thumb_tip_left:
        return 'left'
    elif wrist_right > index_finger_tip_right and wrist_right > thumb_tip_right:
        return 'right'
    else:
        return 'none'

def write_file(file_path,data):
    try: 
        lock = threading.Lock()
        with lock:
            with open(file_path, 'w') as file:
                file.write(data)
        print('Escribiendo')        
    except:
        print('Error')


def analize_video(capture,mediap_draw_points,draw_points,mediap_face_mesh,face_mesh):
    estate_array=[]
    show_mesh=False
    mp_hands = mp.solutions.hands
    hands = mp_hands.Hands()
    mp_drawing = mp.solutions.drawing_utils
    while True:
        #Lectura de frame y el estado (En Python puedo asignar datos a variables de la siguiente forma var1,var2=1,2) 
        estate,frame=capture.read()
        #Efecto espejo
        frame=cv2.flip(frame,1)
        #Procesa el fotograma para entreganos la malla facial
        results=face_mesh.process(frame)
        listaPuntosFaciales=[]
        # Convierte la imagen de BGR a RGB
        image_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

        # Detecta las manos en la imagen
        results_hands = hands.process(image_rgb)

        if results_hands.multi_hand_landmarks:
            for hand_landmarks in results_hands.multi_hand_landmarks:
                landmark_color =  (4, 5, 3)
                connection_color = (163, 140, 47)
                mp_drawing.draw_landmarks(frame, hand_landmarks, mp_hands.HAND_CONNECTIONS,
                                          landmark_drawing_spec=mp_drawing.DrawingSpec(color=landmark_color, thickness=2,
                                                                                         circle_radius=3),
                                          connection_drawing_spec=mp_drawing.DrawingSpec(color=connection_color, thickness=2))
                data_to_send_move = detect_hand_raised(hand_landmarks)
                write_file('file_move.txt',data_to_send_move)

        else:
            write_file('file_move.txt','none')
    
        #Si encuentra un rostro        
        if results.multi_face_landmarks:
            #Para todos los rostros detectados
            for faces in results.multi_face_landmarks:
                #Dibujamos las conecciones de la malla

                mediap_draw_points.draw_landmarks(frame,faces,mediap_face_mesh.FACEMESH_CONTOURS,draw_points,draw_points)
                #Puntos rostro detectado
                for point_id,points in enumerate (faces.landmark):
                    #Alto y ancho de la ventana
                    altoVentana, anchoVentana,variable=frame.shape
                    position_x=int(points.x*anchoVentana)
                    position_y=int(points.y*altoVentana)
                    #Apilamos los puntos faciales en una lista con sus coordenadas
                    listaPuntosFaciales.append([point_id,position_x,position_y])
                    if len(listaPuntosFaciales)==468:
                        facial_analysis_object=AnalisisFacial(listaPuntosFaciales,altoVentana,anchoVentana)
                        show_rotation_axes(listaPuntosFaciales,frame,altoVentana)
                        data_to_send_jump,data_to_send_fire=facial_analysis_object.getLongitudes()
                        write_file('file_jump.txt',data_to_send_jump)
                        write_file('file_fire.txt',data_to_send_fire)

                        time.sleep(0.1)    
        cv2.imshow('Controller VideoGame', frame)                  
        key = cv2.waitKey(1) & 0xFF
        #Codigo Ascii ESC es 27 para cerrar frame
        if key==27:
            break
    #Destruimos cada ventana creada por opencv 
    cv2.destroyAllWindows()
    write_file('file_move.txt','none')
    write_file('file_jump.txt','none')
    write_file('file_fire.txt','none')

               

def show_rotation_axes(listaPuntosFaciales,frame,altoVentana):
    coordenadaCentralX,coordenadaCentralY=listaPuntosFaciales[9][1:]
    coordenadaQuijadaX,coordenadaQuijadaY=listaPuntosFaciales[152][1:]
    cv2.line(frame,pt1=(coordenadaCentralX,coordenadaCentralY),pt2=(coordenadaCentralX,altoVentana),color=(45, 175, 244) )
    cv2.line(frame,pt1=(coordenadaCentralX,coordenadaCentralY),pt2=(coordenadaQuijadaX,coordenadaQuijadaY),color=(45, 175, 244))



#Buena practica de programacion en python (Python ejecuta todos los modulos cargados en orden descendente y declara variables internas __name__ se le declara como main al modulo que se encarga de "correr", al trabajar con esta condicional )
if __name__ == "__main__":
    main()
