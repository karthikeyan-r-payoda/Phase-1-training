
from fastapi import FastAPI, Depends, HTTPException
from sqlalchemy.orm import Session
from models import User
from database import SessionLocal, engine, init_db
import bcrypt
from pydantic import BaseModel
init_db()  

app = FastAPI()

def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

@app.post("/Addusers/")
def create_user(username: str, password: str, role: str, db: Session = Depends(get_db)):
    salt = bcrypt.gensalt()
    hashed_password = bcrypt.hashpw(password.encode('utf-8'), salt)
    user = User(username=username, password=hashed_password.decode('utf-8'), role=role)
    db.add(user)
    db.commit()
    db.refresh(user)
    return user

@app.get("/Getusers/")
def list_users(db: Session = Depends(get_db)):
    return db.query(User).all()


class LoginRequest(BaseModel):
    username: str
    password: str
    
@app.post("/login/")
def login(request: LoginRequest, db: Session = Depends(get_db)):
    user = db.query(User).filter(User.username == request.username).first()
    if user and bcrypt.checkpw(request.password.encode('utf-8'), user.password.encode('utf-8')):
        return {"login": True, "Role": user.role}
    return {"login": False}