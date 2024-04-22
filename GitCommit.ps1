$Message = read-host "ingrese el mensaje del commit"

git add . 

git commit -m "$Message"

write-host "se inserto con exito" 
