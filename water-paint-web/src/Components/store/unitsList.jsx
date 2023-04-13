import { Link } from 'react-router-dom';

const UnitsList = ({ unitsList }) => {
    if (unitsList && Array.isArray(unitsList) && unitsList.length > 0)
        return (
            <>
                {unitsList.map((item, index) => {
                    return (
                        <Link
                            to={`/store/unit?slug=${item.urlSlug}`}
                            title={item.name}
                            className="btn btn-sm btn-outline-secondary me-1"
                            key={index}>
                            {item.name}
                        </Link>
                    );
                })}
            </>
        );
    else return <></>;
};

export default UnitsList;
