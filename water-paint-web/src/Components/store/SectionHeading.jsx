import React from 'react';

function SectionHeading({ type, children }) {
    return <div className={type}>{children}</div>;
}

export default SectionHeading;
