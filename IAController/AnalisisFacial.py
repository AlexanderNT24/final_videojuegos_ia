import math

class AnalisisFacial:
     
    longitudes=None
    altoVentana=None
    anchoVentana=None
    listaPuntosFaciales=None
    longitudBoca=None
    longitudOjoIzquierdo=None
    longitudOjoDerecho=None
    longitudCejaIzquierda=None
    longitudCejaDerecha=None
    longitudBocaHorizontal=None
    emocion=None

    def __init__(self,listaPuntosFaciales,altoVentana,anchoVentana) :  
       self.listaPuntosFaciales=listaPuntosFaciales
       self.altoVentana=altoVentana
       self.anchoVentana=anchoVentana

    def getLongitudes(self):
        xpuntaRostro1,yPuntaRostro1=self.listaPuntosFaciales[93][1:]
        xpuntaRostro2,yPuntaRostro2=self.listaPuntosFaciales[323][1:]
        self.longitudRostro=math.hypot(xpuntaRostro1-xpuntaRostro2,yPuntaRostro1-yPuntaRostro2)
        #print(f"Longitud Rostro {longitudRostro}")
        #Trabajamos con proporciones 240 es el 100%
        porcentaje=self.longitudRostro/240
        #print(f"Porcentaje {int(porcentaje*100)}% Decimal {int(porcentaje*100)}")
        #print(f"Tamaño rostro proporcional {self.longitudRostro/porcentaje}")
        #Segun el identificador tomamos coordenadas en x,y ([n:] desde la posicion n en adelante)
        x1Boca,y1Boca=self.listaPuntosFaciales[13][1:]
        x2Boca,y2Boca=self.listaPuntosFaciales[14][1:]
        #Devuelve la norma de un vector es decir distancia entre dos puntos
        self.longitudBoca=abs(int(math.hypot(x2Boca-x1Boca,y2Boca-y1Boca)/porcentaje))
        #print(f"Longitud Boca:{self.longitudBoca}")
        x1OjoIzquierdo,y1OjoIzquierdo=self.listaPuntosFaciales[159][1:]
        x2OjoIzquierdo,y2OjoIzquierdo=self.listaPuntosFaciales[145][1:]
        #Devuelve la norma de un vector es decir distancia entre dos puntos
        self.longitudOjoIzquierdo=abs(math.hypot(x2OjoIzquierdo-x1OjoIzquierdo,y2OjoIzquierdo-y1OjoIzquierdo)/porcentaje)
        #print(f"Longitud Ojo Izquierdo:{self.longitudOjoIzquierdo}")
        x1OjoDerecho,y1OjoDerecho=self.listaPuntosFaciales[374][1:]
        x2OjoDerecho,y2OjoDerecho=self.listaPuntosFaciales[386][1:]
        #Devuelve la norma de un vector es decir distancia entre dos puntos
        self.longitudOjoDerecho=abs(math.hypot(x2OjoDerecho-x1OjoDerecho,y2OjoDerecho-y1OjoDerecho)/porcentaje)
        
        xbocaIzq,yBocaIzq=self.listaPuntosFaciales[61][1:]
        xbocaDer,yBocaDer=self.listaPuntosFaciales[291][1:]
        self.longitudBocaHorizontal=abs(math.hypot(xbocaIzq-xbocaDer,yBocaIzq-yBocaDer)/porcentaje)
        #print(f" Boca {self.longitudBocaHorizontal}")
        coordenadaCentralX,coordenadaCentralY=self.listaPuntosFaciales[9][1:]
        coordenadaQuijadaX,coordenadaQuijadaY=self.listaPuntosFaciales[152][1:]
        eje=self.altoVentana-coordenadaCentralY
        isRotate=False 
        if (coordenadaCentralX-coordenadaQuijadaX)!=0:
            pendiente=(coordenadaCentralY-coordenadaQuijadaY)/(coordenadaCentralX-coordenadaQuijadaX)  
            if pendiente<-0.1 and pendiente>-4:
                isRotate=True
            elif pendiente>0.1 and pendiente<4:
                isRotate=True  

        if self.longitudBoca<=8:
            mouthIsClose=False
        elif self.longitudBoca>8:
            mouthIsClose=True
 
        if self.longitudOjoIzquierdo<=10:
            leftEyeIsClose=True
        elif  self.longitudOjoIzquierdo>10:  
             leftEyeIsClose=False   

        if self.longitudOjoDerecho<=10:
            rightEyeIsClose=True 
        elif self.longitudOjoDerecho>10:  
             rightEyeIsClose=False

     
        return f'{"jump" if mouthIsClose else "none"}',f'{"fire" if isRotate else "none"}'


