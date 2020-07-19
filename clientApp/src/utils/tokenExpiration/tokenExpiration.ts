import jwt from 'jsonwebtoken';

type Token = { exp: number };

const tokenIsExpired = (token: string): boolean => {
  try {
    const decodedToken: Token = jwt.decode(token) as Token;
    return decodedToken.exp < new Date().getTime() / 1000;
  } catch (error) {
    return true;
  }
};

export default tokenIsExpired;
