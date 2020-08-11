const centimetersInOneInch = 2.54;

export const measurementConverter = {
  convertToCentimeters: (inches: number):number => {
		return inches * centimetersInOneInch
	},
  convertToInches: (centimeters: number):number => {
		return centimeters / centimetersInOneInch;
	},
};
