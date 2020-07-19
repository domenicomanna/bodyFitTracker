import jwt from 'jsonwebtoken';

type Token = { exp: number };

const isExpired = (token: string): boolean => {
  try {
    const decodedToken: Token = jwt.decode(token) as Token;
    return decodedToken.exp < new Date().getTime() / 1000;
  } catch (error) {
    return true;
  }
};

export default isExpired;
