import React, { FunctionComponent } from 'react'

const PageTitle:FunctionComponent = ({children}) => {
    return (
        <h1 style={{textAlign: "center", marginTop: 0}}>{children}</h1>
    )
}

export default PageTitle
