$Message = read-host "ingrese el mensaje del commit"

git add . 

git commit -m "$Message"

git push

write-host "se inserto con exito" 
