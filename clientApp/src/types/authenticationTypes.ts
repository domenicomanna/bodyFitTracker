export type SignInResult = {
    signInWasSuccessful: boolean,
    errorMessage: string,
    token: string,
}

export type SignInFormValues = {
    email: string,
    password: string
}