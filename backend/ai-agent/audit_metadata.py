from app import create_app
from app.models import db
from sqlalchemy.dialects import mysql

app = create_app()

with app.app_context():
    db.configure_mappers()
    dialect = mysql.dialect()

    print("=" * 70)
    print("TOTAL TABLES:", len(db.metadata.tables))
    print("=" * 70)

    for table_name, table in db.metadata.tables.items():
        print(f"\n### TABLE: {table_name}")

        for column in table.columns:
            col_type = column.type.compile(dialect=dialect)
            nullable = "NULL" if column.nullable else "NOT NULL"
            pk = " PK" if column.primary_key else ""
            default = f" DEFAULT={column.server_default.arg}" if column.server_default else ""

            print(f"  {column.name}: {col_type} | {nullable}{pk}{default}")

            for fk in column.foreign_keys:
                print(f"    FK -> {fk.target_fullname}")

        for constraint in table.constraints:
            if constraint.__class__.__name__ == "UniqueConstraint":
                print(f"  UNIQUE: {constraint.name} {list(constraint.columns.keys())}")
            elif constraint.__class__.__name__ == "CheckConstraint":
                print(f"  CHECK: {constraint.name} ({constraint.sqltext})")

    print("\n" + "=" * 70)
    print("RELATIONSHIPS: OK")
    print("METADATA AUDIT: COMPLETED")
    print("=" * 70)
