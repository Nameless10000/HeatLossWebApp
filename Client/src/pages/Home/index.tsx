import Guide from '@/components/Guide';

import { PageContainer } from '@ant-design/pro-components';
import { useModel } from '@umijs/max';
import styles from './index.less';

const HomePage: React.FC = () => {
  return (
    <PageContainer ghost title="bebra">
      <div className={styles.container}>
      </div>
    </PageContainer>
  );
};

export default HomePage;
