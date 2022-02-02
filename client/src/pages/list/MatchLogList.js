import React, { useEffect, useState } from 'react';
import { Table, Button, Modal } from 'antd';
import moment from 'moment';

function MatchLogList({ eventId, visible, closeModal }) {
  const [matchLogList, setMatchLogList] = useState([]);
  const [loading, setloading] = useState(true);

  useEffect(() => {
    if (visible) {
      fetch('http://localhost:5200/api/v1/Match/logs?EventId=' + eventId, {
        method: 'GET',
      })
        .then((response) => {
          if (!response.ok) {
            response.json().then((json) => {
              json.errors.map((error) => message.error(error.message));
            });
          } else {
            response.json().then((data) => {
              data.forEach((match) => (match.key = match.id));
              setMatchLogList(data);
            });
          }
        })
        .finally(() => {
          setloading(false);
        });
    }
  }, [visible]);

  const matchLogListData = loading ? [] : matchLogList;

  const columns = [
    {
      title: 'AffectedColumn',
      dataIndex: 'affectedColumns',
      key: 'affectedColumns',
    },
    {
      title: 'OldValue',
      dataIndex: 'oldValues',
      key: 'oldValues',
    },
    {
      title: 'NewValue',
      dataIndex: 'newValues',
      key: 'newValues',
    },
    {
      title: 'AuditOn',
      dataIndex: 'auditOn',
      key: 'auditOn',
      sorter: (a, b) => moment(a.auditOn).unix() - moment(b.auditOn).unix(),
    },
  ];

  return (
    <Modal
      visible={visible}
      onCancel={closeModal}
      width={1000}
      footer={[]}
    >
      <Table
        columns={columns}
        dataSource={matchLogListData}
        pagination={{ pageSize: 9 }}
        loading={loading}
        scroll={{ y: 500 }}
      />
    </Modal>
  );
}

export default MatchLogList;
