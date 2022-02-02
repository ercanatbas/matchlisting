import React, { useEffect, useState } from 'react';
import PageLayout from '../../layout/PageLayout';
import { Table, Button, Modal } from 'antd';
import moment from 'moment';
import MatchLogList from './MatchLogList';

function MatchList() {
  const [matchList, setMatchList] = useState([]);
  const [loading, setloading] = useState(true);
  const [visible, setVisible] = useState(false);
  const [modalEventId, setModalEventId] = useState('');

  useEffect(() => {
    getData();
  }, []);

  const getData = async () => {
    await fetch('http://localhost:5200/api/v1/Match', {
      method: 'GET',
    })
      .then((response) => {
        if (!response.ok) {
          response.json().then((json) => {
            json.errors.map((error) => message.error(error.message));
          });
        } else {
          response.json().then((data) => {
            data.forEach((match) => (match.key = match.Id));
            setMatchList(data);
          });
        }
      })
      .finally(() => {
        setloading(false);
      });
  };

  const showModal = (Id) => {
    setModalEventId(Id);
    setVisible(true);
  };

  const closeModal = () => {
    setVisible(false);
  };

  const matchListData = loading ? [] : matchList;

  const columns = [
    {
      title: 'ID',
      dataIndex: 'Id',
      key: 'Id',
      sorter: (a, b) => a.Id - b.Id,
    },
    {
      title: 'EventType',
      dataIndex: 'eventType',
      key: 'eventType',
    },
    {
      title: 'Country',
      dataIndex: 'country',
      key: 'country',
    },
    {
      title: 'League',
      dataIndex: 'league',
      key: 'league',
    },
    {
      title: 'HomeTeam',
      dataIndex: 'homeTeam',
      key: 'homeTeam',
    },
    {
      title: 'AwayTeam',
      dataIndex: 'awayTeam',
      key: 'awayTeam',
    },
    {
      title: 'EventTime',
      dataIndex: 'eventTime',
      key: 'eventTime',
      sorter: (a, b) => moment(a.eventTime).unix() - moment(b.eventTime).unix(),
    },
    {
      title: 'Action',
      key: 'action',
      render: (text, record) => {
        return (
          <Button
            style={{ color: '#1890ff', border: 'solid 0px' }}
            onClick={() => showModal(record.Id)}
          >
            <div>View Logs</div>
          </Button>
        );
      },
    },
  ];

  return (
    <PageLayout title="List">
      <React.Fragment>
        <Table
          columns={columns}
          dataSource={matchListData}
          pagination={{ pageSize: 15 }}
          loading={loading}
        />

        <MatchLogList eventId={modalEventId} visible={visible} closeModal={closeModal} />
      </React.Fragment>
    </PageLayout>
  );
}

export default MatchList;
