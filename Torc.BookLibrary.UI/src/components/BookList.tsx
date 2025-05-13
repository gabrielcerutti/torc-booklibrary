import * as React from 'react';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';
import { Box } from '@mui/material';
import type { Book } from '../types/Book';

interface BookListProps {
  books: Book[];
}

const columns: GridColDef[] = [
  { field: 'title', headerName: 'Book Title', flex: 1, minWidth: 150 },
  {
    field: 'author',
    headerName: 'Author',
    flex: 1,
    minWidth: 180,
    valueGetter: (_value, row) => `${row.firstName} ${row.lastName}`,
  },
  { field: 'type', headerName: 'Type', flex: 1, minWidth: 100 },
  { field: 'isbn', headerName: 'ISBN', flex: 1, minWidth: 120 },
  { field: 'category', headerName: 'Category', flex: 1, minWidth: 100 },
  {
    field: 'availableCopies',
    headerName: 'Available Copies',
    flex: 1,
    minWidth: 140,
    valueGetter: (_value, row) =>
      `${row.copiesInUse}/${row.totalCopies}`,
  },
];

const BookList: React.FC<BookListProps> = ({ books }) => {
  return (
    <Box sx={{ width: '100%', mt: 2 }}>
      <DataGrid
        rows={books.map(b => ({ ...b, id: b.bookId }))}
        columns={columns}
        pageSizeOptions={[5, 10, 20]}
        initialState={{
          pagination: { paginationModel: { pageSize: 10, page: 0 } },
        }}
        disableRowSelectionOnClick
        autoHeight
      />
    </Box>
  );
};

export default BookList;