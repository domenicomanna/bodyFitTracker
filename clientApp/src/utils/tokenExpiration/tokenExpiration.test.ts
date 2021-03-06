import jwt from 'jsonwebtoken';
import isExpired from './tokenExpiration';

it('should return true if token is invalid', () => {
  expect(isExpired('')).toBe(true);
});

it('should return false if token is not expired', () => {
  const token = jwt.sign(
    {
      exp: Math.floor(Date.now() / 1000) + (60 * 60), // expires in an hour
      data: 'foobar',
    },
    'secret'
	);
  expect(isExpired(token)).toBe(false);
});

it('should return true if token is expired', () => {
  const token = jwt.sign(
    {
      exp: Math.floor(Date.now() / 1000) + (-60 * 60), // expired an hour ago
      data: 'foobar',
    },
    'secret'
	);
  expect(isExpired(token)).toBe(true);
});
